import { LogFileItemInputOutputActor } from '@/services/ApiClient';
import ChartOptions from '@/components/ChartOptions.vue';

export interface ChartData {
  labels: string[];
  datasets: {
    label: string;
    data: number[];
    backgroundColor: string[];
    borderColor: string[];
    fill: boolean;
  }[];
}

export function countUsernameMatches(data: LogFileItemInputOutputActor[]): Record<string, number> {
  return data.reduce((acc, item) => {
    if (item.userName) {
      acc[item.userName] = (acc[item.userName] || 0) + 1;
    }
    return acc;
  }, {} as Record<string, number>);
}

export function prepareUserCountChartData(usernameCounts: Record<string, number>): ChartData {
  const labels = Object.keys(usernameCounts);
  const counts = Object.values(usernameCounts);

  return {
    labels: labels,
    datasets: [
      {
        label: 'Username Count',
        
        data: counts,
        backgroundColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        borderColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        fill: false
      }
    ]
  };
}

export function prepareHourlyActionsData(data: LogFileItemInputOutputActor[], user: string): ChartData {
  const hours = Array.from({ length: 24 }, (_, i) => i);
  const actionsPerHour = hours.map(hour => {
    return data.filter(item => {
      const matchesUser = user ? item.userName?.includes(user) : true;
      return matchesUser && new Date(item.timestamp).getHours() === hour;
    }).length;
  });

  return {
    labels: [
      '00:00', '01:00', '02:00', '03:00', '04:00', '05:00', '06:00', '07:00', '08:00', '09:00', '10:00', '11:00',
      '12:00', '13:00', '14:00', '15:00', '16:00', '17:00', '18:00', '19:00', '20:00', '21:00', '22:00', '23:00'
    ],
    datasets: [
      {
        label: user ? `Actions by ${user}` : 'Actions per Hour',
        
        data: actionsPerHour,
        backgroundColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        borderColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        fill: false
      }
    ]
  };
}


export function prepareDailyActionsData(data: LogFileItemInputOutputActor[], user: string): ChartData {
  const days = Array.from({ length: 7 }, (_, i) => i);
  const actionsPerDay = days.map(day => {
    return data.filter(item => {
      const matchesUser = user ? item.userName?.includes(user) : true;
      return matchesUser && new Date(item.timestamp).getDay() === day + 1;
    }).length;
  });

  return {
    labels: [
      'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
    datasets: [
      {
        label: user ? `Actions by ${user}` : 'Actions per Day',
        data: actionsPerDay,
        backgroundColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        borderColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        fill: false
      }
    ]
  };
}

export function prepareMonthlyActionsData(data: LogFileItemInputOutputActor[], user: string): ChartData {
  const months = Array.from({ length: 12 }, (_, i) => i);
  const actionsPerMonth = months.map(month => {
    return data.filter(item => {
      const matchesUser = user ? item.userName?.includes(user) : true;
      return matchesUser && new Date(item.timestamp).getMonth() === month;
    }).length;
  });

  return {
    labels: [
      'Januari', 'Februari', 'Mars', 'April', 'Maj', 'Juni',
      'Juli', 'Augusti', 'September', 'Oktober', 'November', 'December'
    ],
    datasets: [
      {
        label: user ? `Actions by ${user}` : 'Actions per Month',
        data: actionsPerMonth,
        backgroundColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        borderColor: ['#c62107', '#65c8e6', '#4d4d4d', '#dfd5bb'],
        fill: false
      }
    ]
  };
}

export function getHourlyActionsOptions(user: string) {
  return {
    scales: {
      x: {
        title: {
          display: true,
          text: 'Time'
        }
      },
      y: {
        title: {
          display: true,
          text: 'Actions'
        }
      }
    },
    plugins: {
      legend: {
        display: true,
        labels: {
          generateLabels: (chart) => {
            const datasets = chart.data.datasets;
            return datasets.map((dataset, i) => ({
              text: dataset.label,
              hidden: !chart.isDatasetVisible(i),
              index: i
            }));
          }
        }
      }
    }
  };
}

export function getMonthlyActionsOptions(user: string) {
  return {
    scales: {
      x: {
        title: {
          display: true,
          text: 'Month'
        }
      },
      y: {
        title: {
          display: true,
          text: 'Actions'
        }
      }
    },
    plugins: {
      legend: {
        display: true,
        labels: {
          generateLabels: (chart) => {
            const datasets = chart.data.datasets;
            return datasets.map((dataset, i) => ({
              text: dataset.label,
              hidden: !chart.isDatasetVisible(i),
              index: i
            }));
          }
        }
      }
    }
  };
}