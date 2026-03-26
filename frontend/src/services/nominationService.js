import axios from "../utils/axiosInstance";

export const nominate = (data) =>
  axios.post("/nomination", data);