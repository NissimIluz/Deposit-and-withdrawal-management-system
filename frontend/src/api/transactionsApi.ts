import axios from "axios";
const API_BASE_URL = "https://localhost:7104/api/Transactions";

export const createTransactionAPI = async (transactionData: any) => {
    return axios.post(`${API_BASE_URL}/process`, transactionData);
}

export const fetchTransactionHistory =async (userId:string) => {
    return axios.get(`${API_BASE_URL}/history/${userId}`);
}