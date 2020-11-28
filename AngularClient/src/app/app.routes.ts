import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ImportacaoComponent } from './importacao/importacao.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'importacao/:id', component: ImportacaoComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRouters {}