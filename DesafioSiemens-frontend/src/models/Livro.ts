
export class Livro {
  id: number;
  titulo: string;
  autorId: number;
  generoId: number;
  autor?: string ;
  genero?: string;

  constructor(
    id: number,
    titulo: string,
    autorId: number,
    generoId: number,
    autor?: string,
    genero?: string
  ) {
    this.id = id;
    this.titulo = titulo;
    this.autorId = autorId;
    this.generoId = generoId;
    this.autor = autor;
    this.genero = genero;
  }

  // Exemplo de método para retornar título formatado
  getTituloFormatado(): string {
    return `${this.titulo} (${this.autor ?? 'Autor Desconhecido'})`;
  }
}