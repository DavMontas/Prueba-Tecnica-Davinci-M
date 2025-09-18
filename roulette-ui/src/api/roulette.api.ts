import axios from 'axios';
import type {
  ResolveBetRequestDto,
  ResolveBetResponseDto,
} from '../types/roulette';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL, // e.g. http://localhost:5000
});

// (Opcional) Solo para mostrar un giro de muestra en UI (no se usa para premios)
export const spin = async () => {
  const { data } = await api.get('/api/roulette/spin');
  return data;
};

export const resolveBet = async (payload: ResolveBetRequestDto) => {
  const { data } = await api.post<ResolveBetResponseDto>('/api/roulette/resolve-bet', payload);
  return data;
};

// Users
export const getBalance = async (name: string) => {
  const { data } = await api.get<{ name: string; balance: number }>(
    `/api/users/${encodeURIComponent(name)}/balance`
  );
  return data;
};

export const saveBalance = async (name: string, delta: number) => {
  const { data } = await api.post<{ name: string; balance: number }>(
    '/api/users/save-balance',
    { name, delta }
  );
  return data;
};
