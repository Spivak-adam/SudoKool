import { Component } from '@angular/core';
import { SudokoolApiService, Board } from '../services/sudokool-api';

@Component({
  selector: 'app-board',
  standalone: true,
  styleUrl: './board.css',
  templateUrl: './board.html',
  imports: [],
})
export class BoardComponent {
  constructor(private api: SudokoolApiService) {}

  row = 9;
  col = 9;

  gameId: number | null = null;

  public board: string[][] = Array.from({ length: this.row }, () => Array(this.col).fill(''));

  invalidCells = new Set<string>();

  startGame() {
    this.api.startGame().subscribe({
      next: (cells: Board[]) => {
        this.board = Array.from({ length: this.row }, () => Array(this.col).fill(''));

        this.invalidCells.clear();
        this.gameId = cells.length > 0 ? cells[0].gameId : null;

        cells.forEach((cell) => {
          this.board[cell.row][cell.column] = cell.input.toString();
        });

        console.log('Generated board:', this.board);
        console.log('Game ID:', this.gameId);
      },
      error: (err) => {
        console.error('Error starting game:', err);
      },
    });
  }

  loadGameById(gameId: number) {
    this.gameId = gameId;

    this.api.getBoards(gameId).subscribe({
      next: (cells: Board[]) => {
        this.board = Array.from({ length: this.row }, () => Array(this.col).fill(''));

        this.invalidCells.clear();

        cells.forEach((cell) => {
          this.board[cell.row][cell.column] = cell.input.toString();
        });

        console.log('Loaded game:', this.board);
      },
      error: (err) => {
        console.error('Load game failed:', err);
      },
    });
  }

  onInputChange(event: Event, rowIndex: number, colIndex: number) {
    const inputElement = event.target as HTMLInputElement;
    const value = inputElement.value;

    this.invalidCells.clear();

    if (value === '') {
      this.board[rowIndex][colIndex] = '';
      return;
    }

    if (!/^[1-9]$/.test(value)) {
      inputElement.value = '';
      this.board[rowIndex][colIndex] = '';
      return;
    }

    const previousValue = this.board[rowIndex][colIndex];

    this.board[rowIndex][colIndex] = '';

    const duplicateCell = this.findDuplicate(rowIndex, colIndex, value);

    if (duplicateCell) {
      this.board[rowIndex][colIndex] = value;

      this.invalidCells.add(this.cellKey(rowIndex, colIndex));
      this.invalidCells.add(duplicateCell);

      return;
    }

    this.board[rowIndex][colIndex] = value;

    if (this.gameId === null) {
      console.error('No gameId found. Start a game first.');
      return;
    }

    this.saveMove(rowIndex, colIndex, value, previousValue);
  }

  private saveMove(rowIndex: number, colIndex: number, value: string, previousValue: string) {
    if (this.gameId === null) {
      return;
    }

    const move: Partial<Board> = {
      gameId: this.gameId,
      row: rowIndex,
      column: colIndex,
      quadrant: this.getQuadrant(rowIndex, colIndex),
      input: Number(value),
      dateEnter: new Date().toISOString(),
    };

    this.api.saveMove(move).subscribe({
      next: (savedMove) => {
        console.log('Saved move:', savedMove);

        if (this.isGameComplete()) {
          alert('You won!');
        }
      },
      error: (err) => {
        console.error('Save move failed:', err);

        this.board[rowIndex][colIndex] = previousValue;
      },
    });
  }

  private findDuplicate(row: number, col: number, value: string): string | null {
    const quad = this.getQuadrant(row, col);

    for (let r = 0; r < 9; r++) {
      for (let c = 0; c < 9; c++) {
        if (r === row && c === col) continue;

        const sameValue = this.board[r][c] === value;
        const sameRow = r === row;
        const sameCol = c === col;
        const sameQuad = this.getQuadrant(r, c) === quad;

        if (sameValue && (sameRow || sameCol || sameQuad)) {
          return this.cellKey(r, c);
        }
      }
    }

    return null;
  }

  private isGameComplete(): boolean {
    for (let row = 0; row < 9; row++) {
      for (let col = 0; col < 9; col++) {
        if (this.board[row][col] === '') {
          return false;
        }
      }
    }

    return this.allRowsValid() && this.allColsValid() && this.allQuadsValid();
  }

  private allRowsValid(): boolean {
    for (let row = 0; row < 9; row++) {
      const nums = this.board[row];

      if (!this.hasOneThroughNine(nums)) {
        return false;
      }
    }

    return true;
  }

  private allColsValid(): boolean {
    for (let col = 0; col < 9; col++) {
      const nums: string[] = [];

      for (let row = 0; row < 9; row++) {
        nums.push(this.board[row][col]);
      }

      if (!this.hasOneThroughNine(nums)) {
        return false;
      }
    }

    return true;
  }

  private allQuadsValid(): boolean {
    for (let startRow = 0; startRow < 9; startRow += 3) {
      for (let startCol = 0; startCol < 9; startCol += 3) {
        const nums: string[] = [];

        for (let r = startRow; r < startRow + 3; r++) {
          for (let c = startCol; c < startCol + 3; c++) {
            nums.push(this.board[r][c]);
          }
        }

        if (!this.hasOneThroughNine(nums)) {
          return false;
        }
      }
    }

    return true;
  }

  private hasOneThroughNine(nums: string[]): boolean {
    const required = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

    return required.every((num) => nums.includes(num));
  }

  private cellKey(row: number, col: number): string {
    return `${row}-${col}`;
  }

  private getQuadrant(row: number, col: number): number {
    return Math.floor(row / 3) * 3 + Math.floor(col / 3);
  }

  isInvalidCell(row: number, col: number): boolean {
    return this.invalidCells.has(this.cellKey(row, col));
  }
}
