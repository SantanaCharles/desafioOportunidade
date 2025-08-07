import api from './api';
import { Autor } from '../models/Autor';

const endpoint = '/autor';

export const getAutores = () => api.get<Autor[]>(endpoint);
export const getAutorById = (id: number) => api.get<Autor>(`${endpoint}/${id}`);
export const createAutor = (autor: Omit<Autor, 'id'>) => api.post<Autor>(endpoint, autor);
export const updateAutor = (id: number, autor: Autor) => api.put(`${endpoint}/${id}`, autor);
export const deleteAutor = (id: number) => api.delete(`${endpoint}/${id}`);