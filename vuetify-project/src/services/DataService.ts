import { ApiClient, LogFileItem } from '@/services/ApiClient';

export async function fetchData(apiClient: ApiClient): Promise<LogFileItem[]> {
  try {
    return await apiClient.logFileItemsAll();
  } catch (error) {
    console.error('Error fetching data:', error);
    throw error;
  }
}