import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import AppRouter from './routes/AppRouter'
import { AuthProvider } from './context/AuthContext'
import ApiContextProvider from './context/ApiContext'
import NavBar from './components/layout/NavBar'
import Header from './components/layout/Header'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <ApiContextProvider>
        <AuthProvider>
          <Header/>
          <NavBar/>
          <AppRouter />
        </AuthProvider>
      </ApiContextProvider>
    </>
  )
}

export default App
