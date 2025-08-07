import type { ILivro } from './Livro';
export interface IAutor {
  id: number;
  nome: string;
  livros?: ILivro[]; // opcional, relação com livros
}