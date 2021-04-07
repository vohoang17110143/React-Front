import axios from 'axios'


const url = {
  baseUrl: "https://localhost:5001/api/v1",
  categories: "/categories/",
  brand: "/brands/",
};

const instance = axios.create({
  baseURL: url.baseUrl,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
});

const api = {
  url,
  instance,
  get: instance.get,
  post: instance.post,
  put: instance.put,
  delete: instance.delete,
};

export default api;

