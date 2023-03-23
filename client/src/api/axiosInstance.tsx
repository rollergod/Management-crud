import axios from "axios";
import React from "react";


export const client = axios.create({
    baseURL: "https://localhost:7212/api"
});
