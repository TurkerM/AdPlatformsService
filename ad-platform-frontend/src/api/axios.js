import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5292/api", // .NET backend adress
});

export default api;
