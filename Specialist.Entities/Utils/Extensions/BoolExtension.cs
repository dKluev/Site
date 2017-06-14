namespace SimpleUtils.Extension {
	public static class BoolExtension {
		public static int ToInt(this bool b) {
			return b ? 1 : 0;
		}
	}
}