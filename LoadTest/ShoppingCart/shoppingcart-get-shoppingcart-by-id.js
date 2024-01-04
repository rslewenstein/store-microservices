import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  vus: 10,
  duration: '30s',
};

export default function() {
  // http.get('https://test.k6.io');
  http.get('http://localhost:5227/api/ShoppingCart/1');
  sleep(1);
}
