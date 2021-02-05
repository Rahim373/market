import axios from "axios";

axios.defaults.baseURL = 'https://localhost:44391/api/';
axios.defaults.responseType = 'json';

const request = {
    get: async (url) => await (await axios.get(url)).data,
    post: async (url, body = {}) => await axios.post(url, body).data,
    put: async (url, body = {}) => await axios.put(url, body).data,
    delete: async (url) => await axios.delete(url).data
};

export default request;