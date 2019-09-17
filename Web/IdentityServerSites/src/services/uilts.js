export default {
  CheckPermiss(requierd, currnet) {
    if (!currnet) return false;
    if (!Array.isArray(currnet)) {
      if (requierd.indexOf(currnet) < 0) {
        return false;
      } else {
        return true;
      }
    }
    currnet.forEach(element => {
      if (requierd.indexOf(element) < 0) return false;
    });
    return true;
  },

  distinct(a) {
    return Array.from(new Set([...a]))
  }
}