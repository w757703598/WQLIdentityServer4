


<script>
// <template>
//   <div>
//     <svgIcon v-if="icon" :iconClass="icon"></svgIcon>
//     <span v-if="title" slot="title">{{title}}</span>
//   </div>
// </template>

import svgIcon from "../../../components/SvgIcon";
export default {
  components: {
    svgIcon
  },
  mounted() {
    console.info(this.icon + ";" + this.title);
  },
  name: "MenuItem",
  functional: true,
  props: {
    icon: {
      type: String,
      default: ""
    },
    title: {
      type: String,
      default: ""
    }
  },
  render(h, context) {
    console.info(context);
    const { icon, title } = context.props;
    const vnodes = [];
    if (icon) {
      vnodes.push(<svg-icon icon-class={icon} />);
    }
    if (title) {
      vnodes.push(<span slot="title">{title}</span>);
    }
    return vnodes;
  }
};
</script>
