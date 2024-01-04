import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  vus: 10,
  duration: '30s',
};

export default function () {
  const url = 'http://localhost:5227/api/ShoppingCart';
  const payload = JSON.stringify(
    {
      "shoppingCartId": 20005,
      "userId": 5667,
      "orders": [
        {
          "productId": 1,
          "quantity": 1,
          "price": 239.90
        }
      ],
      "dateShoppingCart": "2023-12-28T22:43:17.694Z",
      "confirmed": true
    }
  );

  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  http.post(url, payload, params);
}