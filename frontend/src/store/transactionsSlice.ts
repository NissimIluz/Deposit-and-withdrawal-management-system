import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import { createTransactionAPI, fetchTransactionHistory } from "../api/transactionsApi";
import { AppDispatch, RootState } from "../store/store";

interface Transaction {
  id?: string;
  fullNameHebrew: string;
  fullNameEnglish: string;
  birthDate: string;
  idNumber: string;
  transactionType: "deposit" | "withdraw";
  amount: number;
  accountNumber: string;
}

interface TransactionsState {
  transactions: Transaction[];
  history: Transaction[];
  loading: boolean;
  error: string | null;
  userId: string | null;
}

const initialState: TransactionsState = {
  transactions: [],
  history: [],
  loading: false,
  error: null,
  userId: null,
};

// ✅ שליחת טרנזקציה ל-API
export const processTransaction = createAsyncThunk(
  "transactions/processTransaction",
  async (transactionData: Transaction, { rejectWithValue }) => {
    try {
      const response = await createTransactionAPI(transactionData);
      return response.data; // השרת מחזיר את הטרנזקציה החדשה
    } catch (error: any) {
      return rejectWithValue(error.response?.data || "שגיאה בביצוע הפעולה");
    }
  }
);

export const loadTransactionHistory = createAsyncThunk(
  "transactions/loadTransactionHistory",
  async (userId: string) => {
    const response =  await fetchTransactionHistory(userId);
    return response.data;
  }
);

export const setTransactionHistory = createAsyncThunk(
    "transactions/loadTransactionHistory",
    async (userId: string) => {
      return await fetchTransactionHistory(userId);
    }
  );


const transactionsSlice = createSlice({
  name: "transactions",
  initialState,
  reducers: {
    setUserId: (state, action: PayloadAction<string>) => {
      state.userId = action.payload;
    },
    clearTransactionHistory: (state) => {
        state.history = []; // ✅ Reset history
      },
  },
  extraReducers: (builder) => {
    builder
      .addCase(processTransaction.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(processTransaction.fulfilled, (state, action) => {
        state.loading = false;
        state.transactions.push(action.payload); // ✅ הוספת הפעולה לרשימה
      })
      .addCase(processTransaction.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(loadTransactionHistory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadTransactionHistory.fulfilled, (state, action) => {
        console.log(action);
        state.loading = false;
        state.history = action.payload;
      })
      .addCase(loadTransactionHistory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || "שגיאה בטעינת ההיסטוריה";
      })
  },
});

// ✅ Action creator to dispatch loadTransactionHistory when setting userId
export const setUserIdAndLoadHistory =
  (userId: string) => async (dispatch: AppDispatch, getState: () => RootState) => {
    dispatch(setUserId(userId));
    debugger;
    if (/^\d{9}$/.test(userId)){
      dispatch(loadTransactionHistory(userId));
    }
    else {
        dispatch(clearTransactionHistory());
    }
  };

export const { setUserId, clearTransactionHistory  } = transactionsSlice.actions;
export default transactionsSlice.reducer;
