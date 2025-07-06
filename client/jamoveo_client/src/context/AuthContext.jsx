import React, { createContext, useContext, useState, useEffect } from "react";
import { ApiContext } from "./ApiContext";

const AuthContext = createContext({
    user: null,
    isLogged: false,
    login: async () => { },
    logout: () => { },
});

export function AuthProvider({ children }) {
    const { local ,server} = useContext(ApiContext);
    const [user, setUser] = useState(() =>
        JSON.parse(localStorage.getItem("user"))
    );

    const login = async ({ id, password }) => {
        const res = await fetch(local + "api/LogIn/LogIn", {
            method: "POST",
            body: JSON.stringify({ id, password }),
            headers: {
                "Content-Type": "application/json; charset=UTF-8",
                Accept: "application/json; charset=UTF-8",
            },
        })
            .then((res) => {
                if (!res.ok) {
                    throw new Error(`שגיאה בשרת: ${res.status}`);
                }
                return res.json();
            })
            .then((data) => {
                console.log("✅ הצלחה! המחרוזת שהתקבלה מהשרת:", data);
                localStorage.setItem("user", JSON.stringify(data));
                setUser(data);
            })
            .catch((error) => {
                console.log("❌ שגיאה ב-POST:", error);
            });
    };

    const logout = () => {
        localStorage.removeItem("user");
        setUser(null);
    };

    const value = { user, isLogged: !!user, login, logout };
    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export const useAuth = () => useContext(AuthContext);
