import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  vus: 10,
  duration: '30s',
};

export default function() {
  http.put('http://localhost:5013/api/Product/1, 1');
  sleep(1);
}
