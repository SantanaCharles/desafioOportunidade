import type { IAutor } from './Autor';
import type { IGenero } from './Genero';

export interface ILivro {
  id: number;
  titulo: string;
  autorId: number;
  generoId: number;
  autor?: IAutor;   // opcional, relação com Autor
  genero?: IGenero; // opcional, relação com Genero
}