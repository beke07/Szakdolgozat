import axios from 'axios'

const instance = axios.create({
  baseURL: '/api'
})

instance.interceptors.request.use(config => {
  console.log("start");
  return config
})

instance.interceptors.response.use(response => {
  console.log("done");
  return response
})

export default instance
