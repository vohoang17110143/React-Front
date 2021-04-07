import {GET_SIZE,MARK_SIZE,ADD_SIZE} from './type';
import axios from 'axios'


export const getSize= () => async dispatch => {
	try {
		const response = await axios.get(
			'https://localhost:5001/api/v1/brands'
		)
		dispatch({
			type: GET_SIZE,
			payload: response.data
		})
	} catch (error) {
		console.log(error)
	}
}

export const markComplete = id => dispatch => {
  dispatch({
      type: MARK_SIZE,
      payload: id
          })
}






