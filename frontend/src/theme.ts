import { createTheme } from "@mui/material/styles";
import { prefixer } from "stylis";
import stylisRTLPlugin from "stylis-plugin-rtl";

export const theme = createTheme({
  direction: "rtl", // הגדרת כיוון RTL
  typography: {
    fontFamily: "Arial, sans-serif",
  },
  components: {
    MuiTypography: { defaultProps: { dir: "rtl" } },
    MuiTextField: { defaultProps: { dir: "rtl" } },
  },
});

export const rtlStylis = [prefixer, stylisRTLPlugin];
