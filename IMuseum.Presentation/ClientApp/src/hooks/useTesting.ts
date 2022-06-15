import React,{useEffect} from 'react'

export const useTesting = ()=>{
  const [a,setA] = React.useState("")
  useEffect(()=>{
    fetch('https://localhost:7001/api/weatherforecast').then(res=>{console.log('res',res);return res.json()}).then(data=>{
      console.log('aasd',data)
      setA(data)
    })
  },[])
  return a
}