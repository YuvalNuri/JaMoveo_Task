import { createContext, useContext, useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { ApiContext } from "./ApiContext";

const SocketContext = createContext();

export const SocketProvider = ({ children }) => {
  const [connection, setConnection] = useState(null);
  const {local, server} = useContext(ApiContext);

  useEffect(() => {
    const conn = new signalR.HubConnectionBuilder()
      .withUrl(local+"rehearsalHub")
      .build();

    conn.start().then(() => console.log("Connected to hub"));
    setConnection(conn);

    return () => {
      conn.stop();
    };
  }, []);

  return (
    <SocketContext.Provider value={{ connection }}>
      {children}
    </SocketContext.Provider>
  );
};

export const useSocket = () => useContext(SocketContext);
