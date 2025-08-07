import api from './api';
import { Livro } from '../models/Livro';

const endpoint = '/livros';

export const getLivros = () => api.get<Livro[]>(endpoint);
export const getLivroById = (id: number) => api.get<Livro>(`${endpoint}/${id}`);
export const createLivro = (livro: Omit<Livro, 'id'>) => api.post<Livro>(endpoint, livro);
export const updateLivro = (id: number, livro: Livro) => api.put(`${endpoint}/${id}`, livro);
export const deleteLivro = (id: number) => api.delete(`${endpoint}/${id}`);