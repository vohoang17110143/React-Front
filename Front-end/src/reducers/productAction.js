import {GET_PRODUCT,ADD_PRODUCT} from './type';
import axios from 'axios'


export const getProduct = () => async dispatch => {
	try {
		const response = await axios.get(
			'https://localhost:5001/api/v1/products/'
		)
		dispatch({
			type: GET_PRODUCT,
			payload: response.data
		})
	} catch (error) {
		console.log(error)
	}
}


export const addProduct = newProduct => async dispatch =>{
  try {
      await axios.post('https://localhost:5001/api/v1/products',newProduct)
      dispatch({
          type:  ADD_PRODUCT,
          payload: newProduct
      })

  } catch (error) {
      console.log(error.messages)
  }
}

