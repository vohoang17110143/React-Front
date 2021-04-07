import {GET_COLORS,ADD_COLOR} from './type';
import axios from 'axios'


export const getColors = () => async dispatch => {
	try {
		const response = await axios.get(
			'https://localhost:5001/api/v1/colors/'
		)
		dispatch({
			type: GET_COLORS,
			payload: response.data
		})
	} catch (error) {
		console.log(error)
	}
}

export const addColor = newColor => async dispatch =>{
  try {
      await axios.post('https://localhost:5001/api/v1/colors/',newColor)
      dispatch({
          type:  ADD_COLOR,
          payload: newColor
      })

  } catch (error) {
      console.log(error.messages)
  }
}

