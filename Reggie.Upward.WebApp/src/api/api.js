import axios from 'axios';

let base = 'http://127.0.0.1:5001/api';

export const getValues = params => {
  return axios.get(`${base}/values`, {
    params: params
  });
};

export const getBrands = params => {
  return axios.get(`${base}/brand`, {
    params: params
  });
};
