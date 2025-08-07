import axios from 'axios';

const baseURL = `${import.meta.env.VITE_APP_API_BASE_URL}/api/${import.meta.env.VITE_APP_API_VERSION}`;

const api = axios.create({
  baseURL, 
  headers: {
    'Content-Type': 'application/json'
  }
});

api.interceptors.response.use(
  response => response,
  error => {
    if (error.response) {
      const status = error.response.status;

      if (status === 401) {
        // Exemplo: redirecionar para login
        window.location.href = '/login';
      } else if (status === 500) {
        alert('Erro interno no servidor. Tente novamente mais tarde.');
      } else if (status === 403) {
        alert('Acesso negado.');
      }
    } else if (error.request) {
      alert('Sem resposta do servidor. Verifique sua conexão.');
    } else {
      console.error('Erro:', error.message);
    }

    return Promise.reject(error);
  }
);

export default api;