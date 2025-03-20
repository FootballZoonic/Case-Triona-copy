import { ApiClient, LogFileItemInputOutputActor } from '@/services/ApiClient';

const apiClient = new ApiClient('http://localhost:5268');

export const fetchLogFiles = async (params: any): Promise<LogFileItemInputOutputActor[]> => {
  try {
    const { caseParam, userParam, actorParam, typeParam, startDateParam, endDateParam } = params;
    console.log(params);
    const fetchedData = await apiClient.searchAll(caseParam, userParam, actorParam, typeParam, startDateParam, endDateParam);
    console.log(fetchedData);

    // Ensure userName is correctly set
    return fetchedData.map(item => ({
      ...item,
      userName: item.userName || '', // Ensure userName is not null or undefined
    }));
  } catch (error) {
    console.error('Error fetching data:', error);
    return [];
  }
};