import type { ILivro } from '../interfaces/Livro';

export class Autor {
  id: number;
  nome: string;
  livros: ILivro[];

  constructor(id: number, nome: string, livros: ILivro[] = []) {
    this.id = id;
    this.nome = nome;
    this.livros = livros;
  }

  // Exemplo de método útil
  getQuantidadeLivros(): number {
    return this.livros.length;
  }
}