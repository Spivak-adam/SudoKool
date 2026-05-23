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

  getRandomInt(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  public board: string[][] = Array.from({ length: this.row }, () => Array(this.col).fill(''));

  /*public board: number[][] = Array.from(
        {length: this.row}, 
        () => Array.from({length: this.col},
            () => this.getRandomInt(1,9)
        )
    )*/

  onInputChange(event: Event) {
  const input = event.target as HTMLInputElement;

  console.log(input.value);

  input.style.backgroundColor = "yellow";}
}
