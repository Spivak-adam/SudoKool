import { TestBed } from '@angular/core/testing';

import { SudokoolApi } from './sudokool-api';

describe('SudokoolApi', () => {
  let service: SudokoolApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SudokoolApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
