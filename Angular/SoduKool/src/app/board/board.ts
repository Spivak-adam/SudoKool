import { Component } from '@angular/core';

@Component({
  selector: 'app-board',
  standalone: true,
  styleUrl: './board.css',
  templateUrl: './board.html',
  imports: [],
})
export class Board {
  row = 9;
  col = 9;

  /*  getRandomInt(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
  public board: number[][] = Array.from(
        {length: this.row}, 
        () => Array.from({length: this.col},
            () => this.getRandomInt(1,9)
        )
    )*/

  public board: string[][] = Array.from({ length: this.row }, () => Array(this.col).fill(''));

  onInputChange(event: Event, rowIndex: number, colIndex: number) {
    const value = (event.target as HTMLInputElement).value;

    this.board[rowIndex][colIndex] = value;

    console.log('cell-' + rowIndex + '-' + colIndex + ': ' + this.board[rowIndex][colIndex]);
  }
}
