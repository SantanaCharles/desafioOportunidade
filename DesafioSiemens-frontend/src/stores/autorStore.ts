import { create } from 'zustand';
import { Autor } from '../models/Autor';
import { getAutores, createAutor, deleteAutor, updateAutor } from '../services/autorService';

interface AutorStore {
  autores: Autor[];
  loading: boolean;
  erro: string | null;
  carregarAutores: () => Promise<void>;
  adicionarAutor: (autor: Omit<Autor, 'id'>) => Promise<void>;
  editarAutor: (id: number, dados: Autor) => Promise<void>;
  deletarAutor: (id: number) => Promise<void>;
}

export const useAutorStore = create<AutorStore>((set, get) => ({
  autores: [],
  loading: false,
  erro: null,

  carregarAutores: async () => {
    set({ loading: true, erro: null });
    try {
      const response = await getAutores();
      set({ autores: response.data, loading: false });
    } catch (err) {
      set({ erro: 'Erro ao carregar autores', loading: false });
    }
  },

  adicionarAutor: async (autor) => {
    set({ loading: true, erro: null });
    try {
      await createAutor(autor);
      await get().carregarAutores(); // Recarrega lista depois de adicionar
      set({ loading: false });
    } catch (err) {
      set({ erro: 'Erro ao adicionar autor', loading: false });
    }
  },

  deletarAutor: async (id) => {
    set({ loading: true, erro: null });
    try {
      await deleteAutor(id);
      await get().carregarAutores(); // Recarrega lista depois de deletar
      set({ loading: false });
    } catch (err) {
      set({ erro: 'Erro ao deletar autor', loading: false });
    }
  },
  editarAutor: async (id, dados) => {
    set({ loading: true, erro: null });
    try {
      const response = await updateAutor(id, dados);
      console.log('Autor editado com sucesso:', get().autores.map((a) =>
          a.id === id ? response.data as Autor : a
        ));
      await get().carregarAutores(); // Recarrega lista depois de deletar
      set({ loading: false });
   
    } catch (err) {
      set({ erro: 'Erro ao editar autor', loading: false });
    }
  }
}));
