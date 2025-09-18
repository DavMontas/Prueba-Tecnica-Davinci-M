import axios from 'axios';
import type { CalculatePrizeRequest, PrizeResult, SpinResultDto } from '../types/roulette';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

export const spin = async (): Promise<SpinResultDto> => {
  const { data } = await api.get<SpinResultDto>('/api/roulette/spin');
  return data;
};

export const calculatePrize = async (payload: CalculatePrizeRequest): Promise<PrizeResult> => {
  const { data } = await api.post<PrizeResult>('/api/roulette/calculate-prize', payload);
  return data;
};

// Users
export const getBalance = async (name: string): Promise<{ name: string; balance: number }> => {
  const { data } = await api.get<{ name: string; balance: number }>(`/api/users/${encodeURIComponent(name)}/balance`);
  return data;
};

export const saveBalance = async (name: string, delta: number): Promise<{ name: string; balance: number }> => {
  const { data } = await api.post<{ name: string; balance: number }>('/api/users/save-balance', { name, delta });
  return data;
};
