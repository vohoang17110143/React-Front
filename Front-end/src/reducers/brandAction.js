import {GET_BRANDS ,ADD_BRANDS,DELETE_BRANDS,SEARCH_BRANDS} from './type';
import axios from 'axios'


export const getBrand = () => async dispatch => {
	try {
		const response = await axios.get(
			'https://localhost:5001/api/v1/brands/'
		)
		dispatch({
			type: GET_BRANDS,
			payload: response.data
		})
	} catch (error) {
		console.log(error)
	}
}

export const addBrand = newCate => async dispatch =>{
  try {
      await axios.post('https://localhost:5001/api/v1/brands/',newCate)
      dispatch({
          type:  ADD_BRANDS,
          payload: newCate
      })

  } catch (error) {
      console.log(error.messages)
  }
}


export const deleteBrand = id => async dispatch =>{



	try {
		await axios.delete(`https://localhost:5001/api/v1/brands/${id}`)
		dispatch({
			type: DELETE_BRANDS,
			payload: id
		})

	} catch (error) {
		console.log(error.messages)
	}
}



export const searchBrand = name => async dispatch =>{



	try {
		await axios.get(`https://localhost:5001/api/v1/brands/search?name=${name}`)
		dispatch({
			type: SEARCH_BRANDS,
			payload: name
		})

	} catch (error) {
		console.log(error.messages)
	}
}
