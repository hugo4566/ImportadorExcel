import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Importacao } from '../models/importacao';
import { LoteImportacao } from '../models/lote-importacao';
import { Arquivo } from '../models/arquivo';

@Injectable({
  providedIn: 'root'
})
export class ImportacaoService {

  url = 'https://localhost:44346/api/Importacao';

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  getImportacoes(): Observable<LoteImportacao[]> {
    return this.httpClient
      .get<LoteImportacao[]>(this.url)
      .pipe()
  }

  getImportacao(id: number): Observable<Importacao[]> {
    return this.httpClient
      .get<Importacao[]>(this.url + '/' + id)
      .pipe()
  }

  postImportacao(arquivo: Arquivo): Observable<any> {
    return this.httpClient
      .post(this.url, arquivo, this.httpOptions)
      .pipe();
  }
}
