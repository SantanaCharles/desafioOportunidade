import type { ILivro } from "./Livro";

export interface IGenero {
  id: number;
  nome: string;
  livros?: ILivro[]; // opcional, relação com livros
}