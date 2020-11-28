import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Importacao } from '../models/importacao';
import { ImportacaoService } from '../services/importacao.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-importacao',
  templateUrl: './importacao.component.html',
  styleUrls: ['./importacao.component.css']
})
export class ImportacaoComponent implements OnInit {

  displayedColumns = ['dtImportacao', 'nmProduto', 'vlUnitario', 'qtdProduto', 'vlTotal'];
  importacoes: Importacao[];
  loading: boolean = false;

  constructor(
    private importacaoService: ImportacaoService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit(): void {
    this.loading = true;
    this.route.paramMap.subscribe(params => {
      let id: number = +params.get("id");
      this.getImportacoes(id);
    });

  }

  getImportacoes(id: number) {
    this.importacaoService.getImportacao(id).subscribe((importacoes: Importacao[]) => {
      this.loading = false;
      this.importacoes = importacoes;
    }, error => {
      this.loading = false;
      if (error.status !== 404)
        this.snackBar.open("Não foi possível realizar a operação. Tente novamente mais tarde.", "Fechar", { duration: 4000 });
    });
  }

  goBack() {
    this.router.navigate(['/']);
  }
}
