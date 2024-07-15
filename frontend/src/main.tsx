import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { BrowserRouter } from 'react-router-dom'
import { store, StoreContext } from './stores/store.ts'

ReactDOM.createRoot(document.getElementById('root')!).render(
<React.StrictMode>
  <StoreContext.Provider value={store}>
     <BrowserRouter>
         <App />
     </BrowserRouter>
  </StoreContext.Provider>
  </React.StrictMode>,
)
