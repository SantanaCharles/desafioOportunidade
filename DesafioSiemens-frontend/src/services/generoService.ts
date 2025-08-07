import api from './api';
import { Genero } from '../models/Genero';

const endpoint = '/generos';

export const getGeneros = () => api.get<Genero[]>(endpoint);
export const getGeneroById = (id: number) => api.get<Genero>(`${endpoint}/${id}`);
export const createGenero = (genero: Omit<Genero, 'id'>) => api.post<Genero>(endpoint, genero);
export const updateGenero = (id: number, genero: Genero) => api.put(`${endpoint}/${id}`, genero);
export const deleteGenero = (id: number) => api.delete(`${endpoint}/${id}`);