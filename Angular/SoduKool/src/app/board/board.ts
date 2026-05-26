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

  public board: string[][] = Array.from({ length: this.row }, () =>
    Array(this.col).fill('')
  );

  invalidCells = new Set<string>();

  startGame() {
    this.api.startGame().subscribe({
      next: (cells: Board[]) => {
        this.board = Array.from({ length: this.row }, () =>
          Array(this.col).fill('')
        );

        this.invalidCells.clear();

        cells.forEach((cell) => {
          this.board[cell.row][cell.column] = cell.input.toString();

          if (this.gameId === null) {
            this.gameId = cell.gameId;
          }
        });

        console.log('Generated board:', this.board);
        console.log('Game ID:', this.gameId);
      },
      error: (err) => {
        console.error('Error starting game:', err);
      },
    });
  }

  loadGame() {
  if (this.gameId === null) {
    console.error('No game to load.');
    return;
  }

  this.api.getBoards(this.gameId).subscribe({
    next: (cells: Board[]) => {
      this.board = Array.from({ length: this.row }, () =>
        Array(this.col).fill('')
      );

      cells.forEach((cell) => {
        this.board[cell.row][cell.column] =
          cell.input.toString();
      });

      console.log('Loaded game:', this.board);
    },
    error: (err) => {
      console.error('Load game failed:', err);
    },
  });
}

  private cellKey(row: number, col: number): string {
    return `${row}-${col}`;
  }

  private getQuadrant(row: number, col: number): number {
    return Math.floor(row / 3) * 3 + Math.floor(col / 3);
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

  onInputChange(event: Event, rowIndex: number, colIndex: number) {
    const input = event.target as HTMLInputElement;
    const value = input.value;

    this.invalidCells.clear();

    if (value === '') {
      this.board[rowIndex][colIndex] = '';
      return;
    }

    if (!/^[1-9]$/.test(value)) {
      input.value = '';
      this.board[rowIndex][colIndex] = '';
      return;
    }

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
      },
      error: (err) => {
        console.error('Save move failed:', err);
      },
    });
  }

  isInvalidCell(row: number, col: number): boolean {
    return this.invalidCells.has(this.cellKey(row, col));
  }
}