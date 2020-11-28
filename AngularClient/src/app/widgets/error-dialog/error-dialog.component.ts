import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-error-dialog',
  templateUrl: './error-dialog.component.html',
  styleUrls: ['./error-dialog.component.css']
})
export class ErrorDialogComponent implements OnInit {

  title : string;
  content : string;
  errorList : string[];

  constructor(@Inject(MAT_DIALOG_DATA) data) {
    this.title = data.title ?? "Ocorreu um erro!";
    this.content = data.content ?? "Não foi possível realizar a operação. Tente novamente mais tarde.";
    this.errorList = data.errorList ?? [];
   }

  ngOnInit(): void {
  }
}
