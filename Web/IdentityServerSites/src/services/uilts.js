export default {
  CheckPermiss(requierd, currnet) {
    if (!currnet) return false;
    currnet.forEach(element => {
      if (requierd.indexOf(element) < 0) return false;
    });
    return true;
  },

  distinct(a) {
    return Array.from(new Set([...a]))
  }
}