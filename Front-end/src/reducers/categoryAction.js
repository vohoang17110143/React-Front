import {GET_CATEGORIES ,ADD_CATEGORIES,DELETE_CATEGORIES} from './type';
import axios from 'axios'


export const getCategory = () => async dispatch => {
	try {
		const response = await axios.get(
			'https://localhost:5001/api/v1/categories/'
		)
		dispatch({
			type: GET_CATEGORIES,
			payload: response.data
		})
	} catch (error) {
		console.log(error)
	}
}

export const addCategory = newCate => async dispatch =>{
  try {
      await axios.post('https://localhost:5001/api/v1/categories/',newCate)
      dispatch({
          type:  ADD_CATEGORIES,
          payload: newCate
      })

  } catch (error) {
      console.log(error.messages)
  }
}


export const deleteCategory = id => async dispatch =>{



	try {
		await axios.delete(`https://localhost:5001/api/v1/categories/${id}`)
		dispatch({
			type: DELETE_CATEGORIES,
			payload: id
		})

	} catch (error) {
		console.log(error.messages)
	}
}
