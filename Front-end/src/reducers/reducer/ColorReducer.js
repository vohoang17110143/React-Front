import {GET_COLORS,ADD_COLOR} from "./../type";
const initialState = {
  colors: [],
};

const colorReducer =(state = initialState, action) => {

  switch (action.type) {
    case GET_COLORS:
      return {
        ...state,
        colors: action.payload,
      };
    case ADD_COLOR:
      return {
        ...state,
        colors:  [...state.colors, action.payload]
      };
    default:
      return state
  }
}

export default colorReducer
