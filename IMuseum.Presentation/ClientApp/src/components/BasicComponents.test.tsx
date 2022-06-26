import React from 'react'
import { render, screen } from '@testing-library/react'
import {BasicComponent} from './BasicComponents'
import { BrowserRouter, Route, Routes } from 'react-router-dom'

test('renders dummy component with text', ()=>{
  render(<BasicComponent text="hello"/>)
  const linkElement = screen.getByText(/hello/i)
  expect(linkElement).toBeInTheDocument()
})

test('renders outlet',()=>{
  window.history.pushState({}, '', '/child')
  render(
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<BasicComponent text='parent'/>}>
          <Route path='child' element={<BasicComponent text='child'/>}/>
        </Route>
      </Routes>
    </BrowserRouter>
  )
  const childEl = screen.getByText(/child/i)
  const parentEl = screen.getByText(/parent/i)
  expect(childEl).toBeInTheDocument()
  expect(parentEl).toBeInTheDocument()
})