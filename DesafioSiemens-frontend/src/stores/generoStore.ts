import { create } from 'zustand';
import { Genero } from '../models/Genero';
import { getGeneros, createGenero, deleteGenero, updateGenero } from '../services/generoService';

interface GeneroStore {
  generos: Genero[];
  loading: boolean;
  erro: string | null;
  carregarGeneros: () => Promise<void>;
  adicionarGenero: (autor: Omit<Genero, 'id'>) => Promise<void>;
  editarGenero: (id: number, dados: Genero) => Promise<void>;
  deletarGenero: (id: number) => Promise<void>;
}

export const useGeneroStore = create<GeneroStore>((set, get) => ({
  generos: [],
  loading: false,
  erro: null,

  carregarGeneros: async () => {
    set({ loading: true, erro: null });
    try {
      const response = await getGeneros();
      set({ generos: response.data, loading: false });
    } catch (err) {
      set({ erro: 'Erro ao carregar generos', loading: false });
    }
  },

  adicionarGenero: async (genero) => {
    set({ loading: true, erro: null });
    try {
      await createGenero(genero);
      await get().carregarGeneros(); // Recarrega lista depois de adicionar
      set({ loading: false });
    } catch (err) {
      set({ erro: 'Erro ao adicionar genero', loading: false });
    }
  },

  deletarGenero: async (id) => {
    set({ loading: true, erro: null });
    try {
      await deleteGenero(id);
      await get().carregarGeneros(); // Recarrega lista depois de deletar
      set({ loading: false });
    } catch (err) {
      set({ erro: 'Erro ao deletar autor', loading: false });
    }
  },
  editarGenero: async (id, dados) => {
    set({ loading: true, erro: null });
    try {
      const response = await updateGenero(id, dados);
      console.log('Autor editado com sucesso:', get().generos.map((a) =>
          a.id === id ? response.data as Genero : a
        ));
      await get().carregarGeneros(); // Recarrega lista depois de deletar
      set({ loading: false });
   
    } catch (err) {
      set({ erro: 'Erro ao editar autor', loading: false });
    }
  }
}));
