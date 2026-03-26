import axios from "../utils/axiosInstance";

export const sendAppreciation = (data) => {
  return axios.post("/appreciation", data);
};