import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import KeyCloakService from  "./security/KeycloakService.tsx";

const root = ReactDOM.createRoot(document.getElementById('root'));

const RenderApp = () =>
  root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>
  );

KeyCloakService.CallLogin(RenderApp);

  reportWebVitals();
