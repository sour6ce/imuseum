import React from 'react';
import './index.css';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { BasicComponent } from './components/BasicComponents';
import { DashboardLayout } from './ui-components/layouts/DashboardLayout';
import { HomePage } from './pages/HomePage';
import { SubheaderProvider } from './ui-components/layouts/SubheaderProvider';
import Dashboard from './pages/Dashboard';
import { AuthProvider } from './hooks/useAuth';
import Catalog from './pages/Catalog';
import Restoration from './pages/Restoration';


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <BrowserRouter>
    <AuthProvider>
    <SubheaderProvider>
    <Routes>
      <Route path='/home' element={<HomePage/>}/>
      
      <Route path='/gallery' element={<BasicComponent text='Gallery'/>}/>
      <Route path='/gallery/:artworkId/*' element={<BasicComponent text='Artwork Gallery'/>}/>
        <Route path='/' element={<DashboardLayout/>}>
          <Route path='dashboard' element={<Dashboard/>}/>
          
          {/* <Route path='users' element={<BasicComponent text='Users'/>}/>
          <Route path='users/new' element={<BasicComponent text='Users New'/>}/>
          <Route path='users/:userId/*' element={<BasicComponent text='User ID'/>}/> */}

          <Route path='catalog' element={<Catalog/>}/>
          {/* <Route path='catalog/new' element={<BasicComponent text='Artwork New'/>}/>
          <Route path='catalog/:artworkId/*' element={<BasicComponent text='Artwork ID'/>}/> */}

          <Route path='restoring' element={<Restoration/>}/>
          <Route path='restoring/:restoreId/*' element={<BasicComponent text='Restoration ID'/>}/>
          <Route path='restoring/new' element={<BasicComponent text='Restorations'/>}/>

          <Route path='loans' element={<BasicComponent text='Loans'/>}/>
          <Route path='loans/new' element={<BasicComponent text='Loans New'/>}/>
          <Route path='loans/:loanId/*' element={<BasicComponent text='Loan ID'/>}/>

          <Route path='loan-applications' element={<BasicComponent text='Loan Apps'/>}/>
        </Route>
    </Routes>
    </SubheaderProvider>
    </AuthProvider>
  </BrowserRouter>
);

