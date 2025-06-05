import { useEffect, useState, useMemo } from 'react';
import styles from './CurrencyChart.module.css';
import {
  Chart as ChartJS,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement,
  Tooltip,
  Legend,
  Filler,
  Title
} from 'chart.js';
import { Line } from 'react-chartjs-2';
import { usePricesStore } from '../../stores/usePricesStore';

ChartJS.register(LineElement, CategoryScale, LinearScale, PointElement, Tooltip, Legend, Title, Filler);

const intervalToMs = {
  '1min': 60 * 1000,
  '2min': 2 * 60 * 1000,
  '3min': 3 * 60 * 1000,
  '4min': 4 * 60 * 1000,
  '5min': 5 * 60 * 1000
};

const getDateTimeLabel = (value: string) => {
  return new Date(value).toLocaleString('en-GB', {
    weekday: 'short',
    day: '2-digit',
    month: 'short',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    timeZone: 'UTC'
  });
};

export const CurrencyChart = () => {
  const [selectedInterval, setSelectedInterval] = useState<'5min' | '4min' | '3min' | '2min' | '1min'>('5min');

  const prices = usePricesStore((state) => state.prices);
  const error = usePricesStore((state) => state.error);

  const filteredPrices = useMemo(() => {
    if (!prices || prices.length === 0) return [];

    const now = Date.now();
    const intervalMs = intervalToMs[selectedInterval];
    const threshold = now - intervalMs;

    return prices.filter((p) => {
      const pTime = new Date(p.dateTime).getTime();
      return pTime >= threshold;
    });
  }, [prices, selectedInterval]);

  const chartData = {
    labels: filteredPrices.map((p) => getDateTimeLabel(p.dateTime)),
    datasets: [
      {
        data: filteredPrices.map((p) => p.price),
        borderColor: 'rgb(53, 107, 222)',
        backgroundColor: 'rgba(196, 215, 255, 0.5)',
        fill: true
      }
    ]
  };

  const options = {
    responsive: true,
    animation: { duration: 0 },
    plugins: {
      legend: { display: false },
      title: { display: false }
    },
    scales: {
      x: {
        display: false
      },
      y: {
        beginAtZero: false
      }
    }
  };

  return (
    <div className="d-flex flex-column w-50">
      <div className="d-flex justify-content-between mb-3">
        {(['5min', '4min', '3min', '2min', '1min'] as const).map((interval) => (
          <button
            key={interval}
            className={`${styles.chartSelectButton} ${selectedInterval === interval ? styles.selected : ''}`}
            onClick={() => setSelectedInterval(interval)}
            disabled={selectedInterval === interval}
          >
            {interval}
          </button>
        ))}
      </div>

      {error ? <div>Ошибка загрузки цен</div> : <Line data={chartData} options={options} />}
    </div>
  );
};
