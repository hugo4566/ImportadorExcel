import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Arquivo } from '../models/arquivo';
import { Importacao } from '../models/importacao';
import { LoteImportacao } from '../models/lote-importacao';
import { ImportacaoService } from '../services/importacao.service';
import { ErrorDialogComponent } from '../widgets/error-dialog/error-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  MAX_SIZE: number = 1048576*5; // 5MB
  file = {} as Arquivo;
  webFile: any = null;
  loading: boolean = false;

  displayedColumns = ['dtImportacao', 'nrRegistros', 'nrTotalProdutos', 'vlTotalImportado', 'menorDataEntrega','delete'];
  lotes: LoteImportacao[];
  loadingLotes : boolean = false;

  constructor(
    private importacaoService: ImportacaoService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private router: Router,
    ) { }

  ngOnInit(): void {
    this.loadingLotes = true;
    this.getLotes();
  }

  getLotes() {
    this.importacaoService.getImportacoes().subscribe((lotes: LoteImportacao[]) => {
      this.loadingLotes = false;
      this.lotes = lotes;
    }, error => {
      this.loadingLotes = false;
      this.snackBar.open("Não foi possível realizar a operação. Tente novamente mais tarde.","Fechar",{duration: 4000});
    });
  }

  onFileChange(event : any) {
    if(event.target.files[0].size < this.MAX_SIZE)
      this.webFile = event.target.files[0];
    else{
      this.snackBar.open("Arquivo maior do que os 5MB permitidos.","Fechar",{duration: 4000});
    }
  }
  
  uploadFile(){
    if(!this.webFile){
      this.snackBar.open("Nenhum arquivo foi selecionado.","Fechar",{duration: 4000});
    }
    else{
      this.loading = true;
      let reader : FileReader = new FileReader();

      reader.onloadend = (e) => {
        let base64 = reader.result.toString();
        this.file.nome = this.webFile.name;
        this.file.base64 = base64.split(",")[1];
        this.importacaoService
          .postImportacao(this.file)
          .subscribe((result : any) => {
            console.log(result);
            this.loading = false;
            this.router.navigate(['/importacao', result.id]);
          }, error => {
            this.loading = false;
            this.dialog.open(ErrorDialogComponent,{
              data:{
                title: "Não foi possível realizar a importação",
                content: error.error.message,
                errorList: error.error.errorList
              }
            });
          });
      }
      reader.readAsDataURL(this.webFile);
    }
  }
}